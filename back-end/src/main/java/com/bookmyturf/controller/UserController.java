package com.bookmyturf.controller;

import com.bookmyturf.entity.User;
import com.bookmyturf.service.UserService;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/user")
@CrossOrigin(origins = "*")
public class UserController {

    @Autowired
    private UserService userService;

    @PostMapping("/register")
    public User registerUser(@RequestBody User user, 
                             @RequestParam String typeName) {
        return userService.registerUser(user, typeName);
    }
   
 // Login
    @PostMapping("/login")
    public User loginUser(@RequestParam String email,
                          @RequestParam String password) {
        return userService.loginUser(email, password);
    }

    @GetMapping("/all")
    public List<User> fetchAllUsers() {
        return userService.getAllUsers();
    }
}
