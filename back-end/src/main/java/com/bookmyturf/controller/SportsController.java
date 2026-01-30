package com.bookmyturf.controller;

import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import com.bookmyturf.entity.Sports;
import com.bookmyturf.service.SportsService;

@RestController
@RequestMapping("/api/sports")
@CrossOrigin(origins = "http://localhost:3000") // Crucial for React
public class SportsController {

    @Autowired
    private SportsService sportService;

    @GetMapping("/all") // Match the frontend fetch URL
    public List<Sports> getAll() {
        return sportService.getAllSports();
    }
}