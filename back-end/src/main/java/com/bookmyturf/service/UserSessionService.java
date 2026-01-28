package com.bookmyturf.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.bookmyturf.entity.UserSession;
import com.bookmyturf.repository.UserSessionRepository;

@Service
public class UserSessionService {

    @Autowired
    private UserSessionRepository sessionRepository;

    public UserSession createSession(UserSession session) {
        return sessionRepository.save(session);
    }

    public void logout(Integer userId) {
        sessionRepository.deleteByUser_UserID(userId);
    }
}
