package com.wyying.lab.service;

import org.junit.Before;
import org.junit.runner.RunWith;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.AbstractJUnit4SpringContextTests;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

/**
 * dubbo 的单元测试
 *
 * @auther: Feng Yapeng
 * @since: 15-12-21 15:00
 */

@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration(locations = {"classpath*:/applicationContext-dubbo-client.xml"})
public class DubboSpringTest extends AbstractJUnit4SpringContextTests {

	@Before
	public  void initDubboTest(){


	}

}
