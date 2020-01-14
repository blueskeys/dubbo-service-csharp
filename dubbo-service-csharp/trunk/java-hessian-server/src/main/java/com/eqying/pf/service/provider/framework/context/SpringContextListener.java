package com.eqying.pf.service.provider.framework.context;

import com.eqying.pf.service.provider.utils.ICacheHelper;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.ApplicationListener;
import org.springframework.context.event.ContextRefreshedEvent;
import org.springframework.stereotype.Service;
import org.springframework.web.context.support.XmlWebApplicationContext;

import javax.annotation.Resource;

/**
 * 初始化加载对象
 *
 * @author zhangbin
 * @since 2016 -07-29 17:33:11
 */
@Service
public class SpringContextListener implements ApplicationListener<ContextRefreshedEvent> {

	@Value("${service.env}")
	private String env; //运行环境变量
	@Resource
	private ICacheHelper iCacheHelper;

	/**
	 * 初始化话 加载信息显示
	 *
	 * @return the boolean
	 * @author zhangbin
	 * @date 2016 /02/18 10:32:08
	 */
	private boolean printKeyLoadMessage() {
		StringBuilder sb = new StringBuilder();
		sb.append("\r\n========================================================================================================\r\n");
		sb.append("[system init]易企赢平台服务提供者 YQY-PF-PROVIDER Env:[" + env + "] by knet.cn");
		sb.append("\r\n========================================================================================================\r\n");
		System.err.print(sb.toString());
		return true;
	}

	@Override
	public void onApplicationEvent(ContextRefreshedEvent event) {
		if(event.getSource() instanceof XmlWebApplicationContext){
			if("Root WebApplicationContext".equals(((XmlWebApplicationContext) event.getSource()).getDisplayName())){
				printKeyLoadMessage();
				iCacheHelper.InitClear();
			}
		}
	}
}
