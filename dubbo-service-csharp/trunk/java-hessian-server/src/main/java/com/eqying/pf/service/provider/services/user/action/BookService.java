package com.eqying.pf.service.provider.services.user.action;

import com.alibaba.dubbo.config.annotation.Service;
import com.eqying.pf.service.provider.api.BookServiceI;
import com.eqying.pf.service.provider.model.Book;


/**
 * TODO(这个类的作用)
 *
 * @auther: renjunjie
 * @since: 2016/12/12 14:08
 */
@Service(interfaceClass = BookServiceI.class)
public class BookService implements BookServiceI {

	@Override
	public Book getBookById(String id) {
		Book book = new Book();
		book.setId("1");
		book.setName("1");
		return book;
	}
}
