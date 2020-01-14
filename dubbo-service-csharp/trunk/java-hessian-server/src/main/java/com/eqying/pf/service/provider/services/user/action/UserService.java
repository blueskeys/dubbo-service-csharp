package com.eqying.pf.service.provider.services.user.action;

import com.google.common.collect.Lists;

import com.alibaba.dubbo.config.annotation.Service;


import com.eqying.pf.service.provider.api.UserServiceI;
import com.eqying.pf.service.provider.model.User;
import com.eqying.pf.service.provider.utils.ICacheHelper;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.time.Instant;
import java.time.LocalDate;
import java.time.ZoneId;
import java.util.Date;

import javax.annotation.Resource;


/**
 * 一个简单的提供者 基本测试
 * @author zhangbin
 * @version 2016 /06/12 16:34:24
 */
@Service(interfaceClass = UserServiceI.class)
public class UserService implements UserServiceI {

	private static final Logger logger = LoggerFactory.getLogger(UserService.class);

	@Resource
	private ICacheHelper iCacheHelper;


	@Override
	public User getUserInfo(String userId) {
		User u = new User();
		u.setId("1001");
		u.setName("小明");
		u.setAge(20);
		LocalDate dateOfBirth = LocalDate.of(1985, 1, 1);
		ZoneId zone = ZoneId.systemDefault();
		Instant instant = dateOfBirth.atStartOfDay().atZone(zone).toInstant();
		java.util.Date date = Date.from(instant);
		u.setBirthDate(date);
		u.setAddress(Lists.newArrayList("河南郑州","江苏泰州","北京"));
		return u;
	}

	@Override
	public int saveUser(User u) {
		return 0;
	}

	@Override
	public int updateUser(User u) {
		return 0;
	}

	@Override
	public int deleteUser(String userId) {
		return 0;
	}
}
