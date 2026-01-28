package com.bookmyturf.repository;

import com.bookmyturf.entity.UserSession;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface UserSessionRepository extends JpaRepository<UserSession, Integer> {

    Optional<UserSession> findBySessionToken(String sessionToken);

    Optional<UserSession> findByUser_UserIDAndIsActiveTrue(Integer userId);

    void deleteByUser_UserID(Integer userId);
}

