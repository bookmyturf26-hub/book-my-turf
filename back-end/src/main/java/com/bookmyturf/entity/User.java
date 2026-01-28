package com.bookmyturf.entity;

import jakarta.persistence.*;
import java.time.LocalDateTime;

import org.hibernate.annotations.CreationTimestamp;
import org.hibernate.annotations.UpdateTimestamp;

import com.fasterxml.jackson.annotation.JsonIgnore;

@Entity
@Table(
	    name = "USER",
	    uniqueConstraints = {
	        @UniqueConstraint(columnNames = "EmailAddress"),
	        @UniqueConstraint(columnNames = "ContactPhoneNo")
	    }
	)
public class User {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "UserID")
    private Integer userID;

    // Many users can have one user type
    @ManyToOne
    @JoinColumn(name = "UserTypeID", referencedColumnName = "UserTypeID")
    private UserType userType;

    @Column(name = "FirstName", nullable = false)
    private String firstName;

    @Column(name = "LastName", nullable = false)
    private String lastName;

    @Column(name = "EmailAddress", nullable = false, unique = true)
    private String emailAddress;

    @Column(name = "Password", nullable = false)
    private String password;

    @Column(name = "PermanentAddress", nullable = false)
    private String permanentAddress;

    @Column(name = "CityName", nullable = false)
    private String cityName;

    @Column(name = "ContactPhoneNo", nullable = false,unique=true)
    private String contactPhoneNo;

    @JsonIgnore
    @Column(name = "account_status")
    private String accountStatus;
    
    @PrePersist
    private void setDefaults() {
        if (this.accountStatus == null) {
            this.accountStatus = "Active";
        }
    }

    
    @CreationTimestamp
    @Column(name = "CreatedDate", updatable = false)
    private LocalDateTime createdDate;

    @UpdateTimestamp
    @Column(name = "UpdatedDate")
    private LocalDateTime updatedDate;
    
    // Getters and Setters
    public Integer getUserID() { return userID; }
    public void setUserID(Integer userID) { this.userID = userID; }

    public UserType getUserType() { return userType; }
    public void setUserType(UserType userType) { this.userType = userType; }

    public String getFirstName() { return firstName; }
    public void setFirstName(String firstName) { this.firstName = firstName; }

    public String getLastName() { return lastName; }
    public void setLastName(String lastName) { this.lastName = lastName; }

    public String getEmailAddress() { return emailAddress; }
    public void setEmailAddress(String emailAddress) { this.emailAddress = emailAddress; }

    public String getPassword() { return password; }
    public void setPassword(String password) { this.password = password; }

    public String getPermanentAddress() { return permanentAddress; }
    public void setPermanentAddress(String permanentAddress) { this.permanentAddress = permanentAddress; }

    public String getCityName() { return cityName; }
    public void setCityName(String cityName) { this.cityName = cityName; }

    public String getContactPhoneNo() { return contactPhoneNo; }
    public void setContactPhoneNo(String contactPhoneNo) { this.contactPhoneNo = contactPhoneNo; }

    public String getAccountStatus() { return accountStatus; }
    public void setAccountStatus(String accountStatus) { this.accountStatus = accountStatus; }

    public LocalDateTime getCreatedDate() { return createdDate; }
    public void setCreatedDate(LocalDateTime createdDate) { this.createdDate = createdDate; }

    public LocalDateTime getUpdatedDate() { return updatedDate; }
    public void setUpdatedDate(LocalDateTime updatedDate) { this.updatedDate = updatedDate; }
}
