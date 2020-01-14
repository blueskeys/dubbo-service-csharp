package com.wyying.lab.service;

import com.alibaba.dubbo.config.annotation.Reference;
import com.eqying.pf.service.provider.api.IService;
import com.eqying.pf.service.provider.api.UserServiceI;

import org.junit.Test;

/**
 * TODO(这个类的作用)
 *
 * @auther: renjunjie
 * @since: 2016/12/5 17:40
 */
public class DonetServiceTest extends DubboSpringTest {

	@Reference(interfaceClass = IService.class)
	private IService iService;


	@Test
	public void test(){
		System.out.printf(iService.Hello("1001"));
	}
}
