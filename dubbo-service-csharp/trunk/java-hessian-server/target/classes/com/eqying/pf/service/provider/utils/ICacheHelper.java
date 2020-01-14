package com.eqying.pf.service.provider.utils;


import com.eqying.basic.cache.CacheTimeUnit;
import com.eqying.basic.cache.ICache;
import com.google.common.collect.Lists;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import javax.annotation.Resource;
import java.io.Serializable;
import java.util.ArrayList;

/**
 * 缓存帮助类
 *
 * @author zhangbin
 * @since 2016 -07-25 12:46:08
 */
@Service
public class ICacheHelper {
    private static final Logger logger = LoggerFactory.getLogger(ICacheHelper.class);

    @Resource(name = "jedisICache")
    private ICache iCache;
    @Value("${service.env}")
    private String env;

    /**
     * 从缓存获取一个对象
     *
     * @param key the key
     * @param tClass the t class
     * @return the cache
     */
    public <T>T getCache(String key,Class<T> tClass) {
        try {
            Object obj = iCache.get(key + env);
            if(obj!=null){
                try {
                    T result = (T) obj;
                    return result;
                } catch (Exception e) {
                    logger.warn("object convert {} error!",tClass,e);
                }
            }
        } catch (Exception e) {
            logger.warn("object convert {} error!",tClass,e);
        }
        return null;
    }

    /**
     * 添加对象到缓存中
     * @author zhangbin
     * @date 2016 /06/23 19:42:34
     * @param key the key
     * @param object the object
     */
    public void addCacheHour(String key, Serializable object) {
        iCache.put(key + env, object, 1, CacheTimeUnit.HOUR);
    }

    /**
     * 添加对象到缓存中(一天)
     * @author zhangbin
     * @date 2016 /06/23 19:42:34
     * @param key the key
     * @param object the object
     */
    public void addCacheDay(String key, Serializable object) {
        //String json = FastJsonUtils.toJson(object);
        iCache.put(key + env, object, 1, CacheTimeUnit.DAY);
    }

    /**
     * 强制清除
     *
     * @param key the key
     */
    public void clear(String key) {
        iCache.delete(key + env);
    }

    /**
     * 初始化清除的缓存
     */
    public void InitClear() {
        ArrayList<String> clear = Lists.newArrayList();
        clear.add(ICacheConstant.YQY_PF_SERVICE_CHOOSE + env);
        clear.add(ICacheConstant.YQY_PF_SERVICE_SHORT_CUT + env);
        clear.add(ICacheConstant.YQY_PF_SERVICE_SHORT_CUT_DEFAULT + env);
        StringBuilder sb = new StringBuilder();
        sb.append("\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n");
        sb.append("[init clear] we need clear cache env["+ env +"]\r\nclear="+clear.toString());
        sb.append("\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n");
        for(String key:clear){
            iCache.delete(key);
        }
        System.err.print(sb.toString());
    }
}
