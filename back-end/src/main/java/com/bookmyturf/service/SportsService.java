package com.bookmyturf.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.bookmyturf.entity.Sports;
import com.bookmyturf.repository.SportsRepository;

@Service
public class SportsService {

    @Autowired
    private SportsRepository sportRepository;

    public Sports createSport(Sports sport) {
        return sportRepository.save(sport);
    }

    public List<Sports> getAllSports() {
        return sportRepository.findAll();
    }
}
