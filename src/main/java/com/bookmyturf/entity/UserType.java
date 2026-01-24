package com.bookmyturf.entity;

import jakarta.persistence.*;
import java.time.LocalDateTime;

@Entity
@Table(name = "user_type")
public class UserType {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "UserTypeID") // exact DB column
    private Integer userTypeId;

    @Column(name = "TypeName")
    private String typeName;

    @Column(name = "Description")
    private String description;

    @Column(name = "IsActive")
    private Boolean isActive;

    @Column(name = "CreatedDate")
    private LocalDateTime createdDate;

    // Getters & Setters
    public Integer getUserTypeId() { return userTypeId; }
    public void setUserTypeId(Integer userTypeId) { this.userTypeId = userTypeId; }
    public String getTypeName() { return typeName; }
    public void setTypeName(String typeName) { this.typeName = typeName; }
    public String getDescription() { return description; }
    public void setDescription(String description) { this.description = description; }
    public Boolean getIsActive() { return isActive; }
    public void setIsActive(Boolean isActive) { this.isActive = isActive; }
    public LocalDateTime getCreatedDate() { return createdDate; }
    public void setCreatedDate(LocalDateTime createdDate) { this.createdDate = createdDate; }
}
