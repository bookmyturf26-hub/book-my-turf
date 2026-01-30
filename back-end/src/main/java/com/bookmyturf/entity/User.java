package com.bookmyturf.entity;

import jakarta.persistence.*;
import java.time.LocalDateTime;
import java.util.List;
import org.hibernate.annotations.CreationTimestamp;
import com.fasterxml.jackson.annotation.JsonIgnore;

@Entity
@Table(name = "USER")
public class User {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "UserID")
    private Integer userID;

    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "UserTypeID")
    private UserType userType;

    @Column(name = "FirstName", nullable = false)
    private String firstName;

    @Column(name = "LastName", nullable = false)
    private String lastName;

    @Column(name = "EmailAddress", unique = true, nullable = false)
    private String emailAddress;

    @Column(name = "Password", nullable = false)
    private String password;

    @Column(name = "ContactPhoneNo")
    private String contactPhoneNo;

    @Column(name = "CityName")
    private String cityName;

    @Column(name = "PermanentAddress")
    private String permanentAddress;

    @Column(name = "AccountStatus")
    private String accountStatus; // e.g., "Active", "Suspended"

    @CreationTimestamp
    @Column(name = "CreatedDate", updatable = false)
    private LocalDateTime createdDate;

    // Relationship to Turf (An owner can have multiple turfs)
    // We use @JsonIgnore to prevent infinite loops when converting to JSON
    @OneToMany(mappedBy = "turfOwner", cascade = CascadeType.ALL)
    @JsonIgnore
    private List<Turf> turfs;

    // Standard Getters and Setters
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

    public String getContactPhoneNo() { return contactPhoneNo; }
    public void setContactPhoneNo(String contactPhoneNo) { this.contactPhoneNo = contactPhoneNo; }

    public String getCityName() { return cityName; }
    public void setCityName(String cityName) { this.cityName = cityName; }

    public String getPermanentAddress() { return permanentAddress; }
    public void setPermanentAddress(String permanentAddress) { this.permanentAddress = permanentAddress; }

    public String getAccountStatus() { return accountStatus; }
    public void setAccountStatus(String accountStatus) { this.accountStatus = accountStatus; }

    public LocalDateTime getCreatedDate() { return createdDate; }

    public List<Turf> getTurfs() { return turfs; }
    public void setTurfs(List<Turf> turfs) { this.turfs = turfs; }
}