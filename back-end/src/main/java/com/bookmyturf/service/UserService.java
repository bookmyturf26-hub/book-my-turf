package com.bookmyturf.service;

import com.bookmyturf.dto.UserRegisterDTO;
import com.bookmyturf.entity.User;      // 1. MUST HAVE THIS IMPORT
import com.bookmyturf.entity.UserType;  // 2. MUST HAVE THIS IMPORT
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

    // The red line is here if 'User' is not imported or return is missing
    public User registerUser(UserRegisterDTO dto) {
        
        // Fetch UserType from DB
        UserType userType = userTypeRepository.findById(dto.getUserTypeId())
            .orElseThrow(() -> new RuntimeException("UserType not found"));

        User user = new User();
        user.setFirstName(dto.getFirstName());
        user.setLastName(dto.getLastName());
        user.setEmailAddress(dto.getEmailAddress());
        user.setPassword(dto.getPassword());
        user.setContactPhoneNo(dto.getContactPhoneNo());
        user.setCityName(dto.getCityName());
        user.setPermanentAddress(dto.getPermanentAddress());
        user.setUserType(userType);
        user.setAccountStatus("Active");

        // THIS 'return' statement MUST match the 'User' return type above
        return userRepository.save(user); 
    }

    public User loginUser(String email, String password) {
        return userRepository.findByEmailAddress(email)
                .filter(u -> u.getPassword().equals(password))
                .orElseThrow(() -> new RuntimeException("Invalid credentials"));
    }

    public List<User> getAllUsers() {
        return userRepository.findAll();
    }
}