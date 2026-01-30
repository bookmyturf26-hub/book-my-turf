package com.bookmyturf.controller;

import com.bookmyturf.dto.UserRegisterDTO;
import com.bookmyturf.dto.UserLoginDTO;
import com.bookmyturf.entity.User;
import com.bookmyturf.service.UserService;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List; // MUST HAVE THIS IMPORT

@CrossOrigin(origins = "http://localhost:3000")
@RestController
@RequestMapping("/api/auth")
public class AuthController {

    @Autowired
    private UserService userService;

    @PostMapping("/register")
    public ResponseEntity<String> registerUser(@Valid @RequestBody UserRegisterDTO dto) {
        try {
            // Now this works because the service returns a User object instead of nothing
            userService.registerUser(dto); 
            return ResponseEntity.ok("User registered successfully");
        } catch (RuntimeException e) {
            return ResponseEntity.badRequest().body(e.getMessage());
        }
    }

    @PostMapping("/login")
    public ResponseEntity<?> loginUser(@Valid @RequestBody UserLoginDTO dto) {
        try {
            User user = userService.loginUser(dto.getEmailAddress(), dto.getPassword());
            return ResponseEntity.ok(user);
        } catch (RuntimeException e) {
            return ResponseEntity.status(401).body(e.getMessage());
        }
    }

    @GetMapping("/all")
    public List<User> fetchAllUsers() {
        return userService.getAllUsers(); // This calls the method in Service
    }
}