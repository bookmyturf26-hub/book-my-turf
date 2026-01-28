package com.bookmyturf.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.bookmyturf.entity.TurfPhotos;
import com.bookmyturf.repository.TurfPhotoRepository;

@Service
	public class TurfPhotoService {

	    @Autowired
	    private TurfPhotoRepository turfPhotoRepository;

	    public TurfPhotos addPhoto(TurfPhotos photo) {
	        return turfPhotoRepository.save(photo);
	    }

	    public List<TurfPhotos> getByTurf(Integer turfId) {
	        return turfPhotoRepository.findByTurf_TurfId(turfId);
	    }
	}



