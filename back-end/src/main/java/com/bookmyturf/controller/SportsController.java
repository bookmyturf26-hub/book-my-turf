package com.bookmyturf.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.bookmyturf.entity.Sports;
import com.bookmyturf.service.SportsService;

public class SportsController {
	@RestController
	@RequestMapping("/api/sports")
	public class SportController {

	    @Autowired
	    private SportsService sportService;

	    @PostMapping
	    public Sports create(@RequestBody Sports sport) {
	        return sportService.createSport(sport);
	    }

	    @GetMapping
	    public List<Sports> getAll() {
	        return sportService.getAllSports();
	    }
	}


}
