package com.bookmyturf.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.bookmyturf.entity.UserSession;
import com.bookmyturf.service.UserSessionService;

@RestController
	@RequestMapping("/api/sessions")
	public class UserSessionController {

	    @Autowired
	    private UserSessionService sessionService;

	    @PostMapping
	    public UserSession create(@RequestBody UserSession session) {
	        return sessionService.createSession(session);
	    }

	    @DeleteMapping("/logout/{userId}")
	    public void logout(@PathVariable Integer userId) {
	        sessionService.logout(userId);
	    }
	}

