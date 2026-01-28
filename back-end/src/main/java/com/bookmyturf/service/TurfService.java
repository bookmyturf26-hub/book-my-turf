package com.bookmyturf.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.bookmyturf.entity.Turf;
import com.bookmyturf.enums.TurfStatus;
import com.bookmyturf.repository.TurfRepository;


@Service
public class TurfService {

    @Autowired
    private TurfRepository turfRepository;

    public Turf createTurf(Turf turf) {
        turf.setTurfStatus(TurfStatus.Active);
        return turfRepository.save(turf);
    }

    public List<Turf> getAllTurfs() {
        return turfRepository.findAll();
    }

    public Turf getById(Integer id) {
        return turfRepository.findById(id)
                .orElseThrow(() -> new RuntimeException("Turf not found"));
    }
}

