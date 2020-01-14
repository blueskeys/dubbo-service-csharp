package com.eqying.pf.service.provider.model;

import java.io.Serializable;
import java.util.Date;
import java.util.List;

/**
 * TODO(这个类的作用)
 *
 * @auther: renjunjie
 * @since: 2016/12/5 17:08
 */
public class User implements Serializable{

	private String       id;
	private String       name;
	private Date         birthDate;
	private long         age;
	private List<String> address;

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public Date getBirthDate() {
		return birthDate;
	}

	public void setBirthDate(Date birthDate) {
		this.birthDate = birthDate;
	}

	public long getAge() {
		return age;
	}

	public void setAge(long age) {
		this.age = age;
	}

	public List<String> getAddress() {
		return address;
	}

	public void setAddress(List<String> address) {
		this.address = address;
	}

	public User() {
	}

	public User(String id, String name, Date birthDate, long age, List<String> address) {
		this.id = id;
		this.name = name;
		this.birthDate = birthDate;
		this.age = age;
		this.address = address;
	}

	@Override
	public String toString() {
		return "User{" + "id='" + id + '\'' + ", name='" + name + '\'' + ", birthDate=" + birthDate + ", age=" + age + ", address="
			   + address + '}';
	}
}
