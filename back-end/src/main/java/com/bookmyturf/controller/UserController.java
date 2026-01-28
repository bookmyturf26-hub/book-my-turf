package com.bookmyturf.controller;

import com.bookmyturf.dto.UserLoginDTO;
import com.bookmyturf.dto.UserRegisterDTO;
import com.bookmyturf.entity.User;
import com.bookmyturf.service.UserService;

import jakarta.validation.Valid;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/user")
@CrossOrigin(origins = "*")
public class UserController {

    @Autowired
    private UserService userService;

   


    @PostMapping("/register")
    public ResponseEntity<String> registerUser(@Valid @RequestBody UserRegisterDTO dto) {

        User savedUser = userService.registerUser(dto);

        return ResponseEntity.ok("User registered successfully");
    }
    
    
    @PostMapping("/login")
    public ResponseEntity<User> loginUser(@RequestBody UserLoginDTO dto) {

        User user = userService.loginUser(
                dto.getEmailAddress(),
                dto.getPassword()
        );
        return ResponseEntity.ok(user);
    }

    @GetMapping("/all")
    public List<User> fetchAllUsers() {
        return userService.getAllUsers();
    }
}
