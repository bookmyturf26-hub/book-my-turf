package com.bookmyturf.service;

import com.bookmyturf.entity.User;
import com.bookmyturf.entity.UserType;
import com.bookmyturf.repository.UserRepository;
import com.bookmyturf.repository.UserTypeRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class UserService {

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private UserTypeRepository userTypeRepository;

    // Register a new user
    public User registerUser(User user, String typeName) {
        // 1. Check if email already exists
        if (userRepository.findByEmailAddress(user.getEmailAddress()) != null) {
            throw new RuntimeException("Email already registered");
        }

        // 2. Get UserType by name (Admin / Player / TurfOwner)
        UserType userType = userTypeRepository.findByTypeName(typeName);
        if (userType == null) {
            throw new RuntimeException("Invalid user type");
        }

        // 3. Assign UserType to user
        user.setUserType(userType);

        // 4. Save user
        return userRepository.save(user);
    }
    
 // Login user (basic – no JWT)
    public User loginUser(String email, String password) {

        User user = userRepository.findByEmailAddress(email);

        if (user == null) {
            throw new RuntimeException("User not found");
        }

        if (!user.getPassword().equals(password)) {
            throw new RuntimeException("Invalid password");
        }

        if (!"active".equalsIgnoreCase(user.getAccountStatus())) {
            throw new RuntimeException("Account is inactive");
        }

        return user;
    }


    // Fetch all users
    public List<User> getAllUsers() {
        return userRepository.findAll();
    }
}
