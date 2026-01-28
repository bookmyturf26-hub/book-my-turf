package com.bookmyturf.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.bookmyturf.entity.Amenity;
import com.bookmyturf.repository.AmenityRepository;

@Service
	public class AmenityService {

	    @Autowired
	    private AmenityRepository amenityRepository;

	    public Amenity createAmenity(Amenity amenity) {
	        return amenityRepository.save(amenity);
	    }

	    public List<Amenity> getAllAmenities() {
	        return amenityRepository.findAll();
	    }
	}



