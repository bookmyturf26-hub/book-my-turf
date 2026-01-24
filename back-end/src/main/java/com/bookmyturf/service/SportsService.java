package com.bookmyturf.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.bookmyturf.entity.Sports;
import com.bookmyturf.repository.SportsRepository;

@Service
public class SportsService {

    @Autowired
    private SportsRepository sportsRepository;

    /**
     * Add new sport
     */
    public Sports addSport(Sports sports) {
        return sportsRepository.save(sports); // ✅ correct
    }
}
