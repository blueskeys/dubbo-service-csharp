package com.eqying.pf.service.provider.api;

import com.eqying.pf.service.provider.model.User;

/**
 * TODO(这个类的作用)
 *
 * @auther: renjunjie
 * @since: 2016/12/5 17:07
 */
public interface UserServiceI {

	public User getUserInfo(String userId);

	public int saveUser(User u);

	public int updateUser(User u);

	public int deleteUser(String userId);

}
