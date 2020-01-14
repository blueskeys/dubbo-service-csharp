package com.eqying.pf.service.provider.model;

import java.io.Serializable;

/**
 * TODO(这个类的作用)
 *
 * @auther: renjunjie
 * @since: 2016/12/12 11:08
 */
public class Order implements Serializable {

	private String id;
	private String orderName;
	private User user;

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getOrderName() {
		return orderName;
	}

	public void setOrderName(String orderName) {
		this.orderName = orderName;
	}

	public User getUser() {
		return user;
	}

	public void setUser(User user) {
		this.user = user;
	}
}
