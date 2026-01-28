
	package com.bookmyturf.dto;
	import jakarta.validation.constraints.NotBlank;
	import jakarta.validation.constraints.Email;
	import jakarta.validation.constraints.Size;

	public class UserRegisterDTO {
		 @NotBlank(message = "First name is required")
		    private String firstName;

		    @NotBlank(message = "Email is required")
		    @Email
		    private String emailAddress;

		    @Size(min = 10, max = 15)
		    private String contactPhoneNo;
	   
	    private String lastName;
	    
	    private String password;
	    private String permanentAddress;
	    private String cityName;
	   
	    private String userType;

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

	    public String getUserType() { return userType; }
	    public void setUserType(String userType) { this.userType = userType; }
	}


