package com.eqying.pf.service.provider.services.user.action;

import com.google.common.collect.Lists;

import com.alibaba.dubbo.config.annotation.Service;
import com.eqying.pf.service.provider.api.OrderServiceI;
import com.eqying.pf.service.provider.api.UserServiceI;
import com.eqying.pf.service.provider.model.Order;
import com.eqying.pf.service.provider.model.User;

import java.time.Instant;
import java.time.LocalDate;
import java.time.ZoneId;
import java.util.Date;

/**
 * TODO(这个类的作用)
 *
 * @auther: renjunjie
 * @since: 2016/12/12 11:06
 */
@Service(interfaceClass = OrderServiceI.class)
public class OrderService implements OrderServiceI {


	@Override
	public Order getOrderById(String id) {
		User u = new User();
		u.setId("1001");
		u.setName("小明");
		u.setAge(20);
		LocalDate dateOfBirth = LocalDate.of(1985, 1, 1);
		ZoneId zone = ZoneId.systemDefault();
		Instant instant = dateOfBirth.atStartOfDay().atZone(zone).toInstant();
		java.util.Date date = Date.from(instant);
		u.setBirthDate(date);
		u.setAddress(Lists.newArrayList("河南郑州", "江苏泰州", "北京"));

		Order o = new Order();
		o.setId("1");
		o.setUser(u);
		return o;
	}
}
