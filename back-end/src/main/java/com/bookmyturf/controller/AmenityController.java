package com.bookmyturf.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.bookmyturf.entity.Amenity;
import com.bookmyturf.service.AmenityService;

@RestController
@RequestMapping("/api/amenities")
public class AmenityController {

    @Autowired
    private AmenityService amenityService;

    @PostMapping
    public Amenity create(@RequestBody Amenity amenity) {
        return amenityService.createAmenity(amenity);
    }

    @GetMapping
    public List<Amenity> getAll() {
        return amenityService.getAllAmenities();
    }
}


