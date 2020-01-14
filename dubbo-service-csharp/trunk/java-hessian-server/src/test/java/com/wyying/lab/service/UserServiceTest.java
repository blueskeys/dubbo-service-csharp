package com.wyying.lab.service;

import com.alibaba.dubbo.config.annotation.Reference;
import com.eqying.pf.service.provider.api.UserServiceI;

import org.junit.Test;

/**
 * TODO(这个类的作用)
 *
 * @auther: renjunjie
 * @since: 2016/12/5 17:40
 */
public class UserServiceTest extends DubboSpringTest {

	@Reference(interfaceClass = UserServiceI.class, async = true)
	private UserServiceI userApiAction;


	@Test
	public void test(){
		System.out.printf(userApiAction.getUserInfo("1001").toString());
	}
}
