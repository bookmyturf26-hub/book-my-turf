package com.bookmyturf.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.bookmyturf.entity.Turf;
import com.bookmyturf.repository.TurfRepository;

@Service
public class TurfService {

    @Autowired
    private TurfRepository turfRepository;

    public Turf addTurf(Turf turf) {
        return turfRepository.save(turf);
    }
}
