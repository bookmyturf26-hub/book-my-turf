package com.bookmyturf.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.bookmyturf.entity.TurfPhotos;
import com.bookmyturf.service.TurfPhotoService;

@RestController
	@RequestMapping("/api/turf-photos")
	public class TurfPhotoController {

	    @Autowired
	    private TurfPhotoService turfPhotoService;

	    @PostMapping
	    public TurfPhotos upload(@RequestBody TurfPhotos photo) {
	        return turfPhotoService.addPhoto(photo);
	    }

	    @GetMapping("/turf/{turfId}")
	    public List<TurfPhotos> getByTurf(@PathVariable Integer turfId) {
	        return turfPhotoService.getByTurf(turfId);
	    }
	}



