package com.bookmyturf.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.bookmyturf.dto.TurfCreateDTO;
import com.bookmyturf.entity.Turf;
import com.bookmyturf.service.TurfService;

@RestController
	@RequestMapping("/api/turfs")
	public class TurfController {

	    @Autowired
	    private TurfService turfService;

	    @PostMapping
	    public Turf create(@RequestBody Turf turf) {
	        return turfService.createTurf(turf);
	    }
	  

	  


	    @GetMapping
	    public List<Turf> getAll() {
	        return turfService.getAllTurfs();
	    }

	    @GetMapping("/{id}")
	    public Turf get(@PathVariable Integer id) {
	        return turfService.getById(id);
	    }
	}



