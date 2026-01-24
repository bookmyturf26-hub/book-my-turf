	package com.bookmyturf.entity;

	import jakarta.persistence.*;
	import java.time.LocalDateTime;

	@Entity
	@Table(name = "USER_SESSION")
	public class UserSession {

	    @Id
	    @GeneratedValue(strategy = GenerationType.IDENTITY)
	    @Column(name = "SessionID")
	    private Integer sessionID;

	    @ManyToOne
	    @JoinColumn(name = "UserID", nullable = false)
	    private User user;

	    @Column(name = "SessionToken", nullable = false, unique = true, length = 255)
	    private String sessionToken;

	    @Column(name = "LoginTime", nullable = false)
	    private LocalDateTime loginTime;

	    @Column(name = "ExpiryTime", nullable = false)
	    private LocalDateTime expiryTime;

	    @Column(name = "IsActive", nullable = false)
	    private Boolean isActive;

	    /* ---------- No-Arg Constructor (MANDATORY) ---------- */
	    public UserSession() {
	        this.loginTime = LocalDateTime.now();
	        this.isActive = true;
	    }

	    /* ---------- Getters & Setters ---------- */

	    public Integer getSessionID() {
	        return sessionID;
	    }

	    public void setSessionID(Integer sessionID) {
	        this.sessionID = sessionID;
	    }

	    public User getUser() {
	        return user;
	    }

	    public void setUser(User user) {
	        this.user = user;
	    }

	    public String getSessionToken() {
	        return sessionToken;
	    }

	    public void setSessionToken(String sessionToken) {
	        this.sessionToken = sessionToken;
	    }

	    public LocalDateTime getLoginTime() {
	        return loginTime;
	    }

	    public void setLoginTime(LocalDateTime loginTime) {
	        this.loginTime = loginTime;
	    }

	    public LocalDateTime getExpiryTime() {
	        return expiryTime;
	    }

	    public void setExpiryTime(LocalDateTime expiryTime) {
	        this.expiryTime = expiryTime;
	    }

	    public Boolean getIsActive() {
	        return isActive;
	    }

	    public void setIsActive(Boolean isActive) {
	        this.isActive = isActive;
	    }
	}


