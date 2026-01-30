package com.bookmyturf.entity;

import jakarta.persistence.*;

@Entity
@Table(name = "USER_TYPE")
public class UserType {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "UserTypeID")
    private Integer userTypeID; // This becomes the JSON key

    @Column(name = "TypeName", nullable = false)
    private String typeName;

    // Getters and Setters
    public Integer getUserTypeID() { return userTypeID; }
    public void setUserTypeID(Integer userTypeID) { this.userTypeID = userTypeID; }

    public String getTypeName() { return typeName; }
    public void setTypeName(String typeName) { this.typeName = typeName; }
}