package com.wyying.lab.service;

import com.caucho.hessian.client.HessianProxyFactory;
import com.eqying.pf.service.provider.api.IService;
import com.eqying.pf.service.provider.api.UserServiceI;

import org.junit.Test;

import java.net.MalformedURLException;

/**
 * TODO(这个类的作用)
 *
 * @auther: renjunjie
 * @since: 2016/12/6 10:24
 */
public class HessianClientTest {



	public  void testjava() {
		String url = "http://192.168.100.189:8080/com.eqying.pf.service.provider.api.UserServiceI";
		HessianProxyFactory factory = new HessianProxyFactory();
		try {
			UserServiceI userServiceI = (UserServiceI) factory.create(UserServiceI.class, url);
			System.out.println(userServiceI.getUserInfo("1001"));
		} catch (MalformedURLException e) {
			e.printStackTrace();
		}

	}

	@Test
	public  void testdonet() {
		String url = "http://192.168.100.10:32269/hessiantest.hessian";
		HessianProxyFactory factory = new HessianProxyFactory();
		try {
			IService iService = (IService) factory.create(IService.class, url);
			System.out.println(iService.Hello("1001"));
		} catch (MalformedURLException e) {
			e.printStackTrace();
		}

	}
}
