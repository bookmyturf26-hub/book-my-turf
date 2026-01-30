package com.bookmyturf.dto;

public class UserRegisterDTO {
    private String firstName;
    private String lastName;
    private String emailAddress;
    private String password;
    private String contactPhoneNo;
    private String cityName;
    private String permanentAddress;
    private Integer userTypeId; // MUST match React: userTypeId

    // Standard Getters and Setters
    public String getFirstName() { return firstName; }
    public void setFirstName(String f) { this.firstName = f; }
    public String getLastName() { return lastName; }
    public void setLastName(String l) { this.lastName = l; }
    public String getEmailAddress() { return emailAddress; }
    public void setEmailAddress(String e) { this.emailAddress = e; }
    public String getPassword() { return password; }
    public void setPassword(String p) { this.password = p; }
    public String getContactPhoneNo() { return contactPhoneNo; }
    public void setContactPhoneNo(String c) { this.contactPhoneNo = c; }
    public String getCityName() { return cityName; }
    public void setCityName(String city) { this.cityName = city; }
    public String getPermanentAddress() { return permanentAddress; }
    public void setPermanentAddress(String a) { this.permanentAddress = a; }
    public Integer getUserTypeId() { return userTypeId; }
    public void setUserTypeId(Integer id) { this.userTypeId = id; }
}