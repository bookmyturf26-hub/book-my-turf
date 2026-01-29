package com.bookmyturf.dto;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.Size;

public class UserLoginDTO {

	@NotBlank
	@Email
    private String emailAddress;

	@NotBlank
	@Size(min = 6, message = "Password must be at least 6 characters")
	private String password;


    // Getters and Setters
    public String getEmailAddress() { return emailAddress; }
    public void setEmailAddress(String emailAddress) { this.emailAddress = emailAddress; }

    public String getPassword() { return password; }
    public void setPassword(String password) { this.password = password; }
}
