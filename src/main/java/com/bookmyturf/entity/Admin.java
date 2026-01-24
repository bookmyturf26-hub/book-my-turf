package com.bookmyturf.entity;

import jakarta.persistence.*;

@Entity
@Table(name = "ADMIN")
public class Admin {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer adminID;

    @OneToOne
    @JoinColumn(name = "UserID")
    private User user;

    private String userName;

    public Admin() {}

    public Admin(Integer adminID, User user, String userName) {
        this.adminID = adminID;
        this.user = user;
        this.userName = userName;
    }

    public Integer getAdminID() { return adminID; }
    public void setAdminID(Integer adminID) { this.adminID = adminID; }

    public User getUser() { return user; }
    public void setUser(User user) { this.user = user; }

    public String getUserName() { return userName; }
    public void setUserName(String userName) { this.userName = userName; }
}
