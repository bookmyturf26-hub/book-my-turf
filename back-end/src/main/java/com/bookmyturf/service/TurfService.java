package com.bookmyturf.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.bookmyturf.dto.TurfCreateDTO;
import com.bookmyturf.entity.Turf;
import com.bookmyturf.entity.User;
import com.bookmyturf.enums.TurfStatus;
import com.bookmyturf.repository.TurfRepository;
import com.bookmyturf.repository.UserRepository;

@Service
public class TurfService {

    @Autowired
    private TurfRepository turfRepository;

    @Autowired
    private UserRepository userRepository;

    // Simple create (if you ever use it)
    public Turf createTurf(Turf turf) {
        turf.setTurfStatus(TurfStatus.Active);
        return turfRepository.save(turf);
    }

    // ✅ Create Turf by Turf Owner (MAIN METHOD)
    public Turf addTurf(TurfCreateDTO dto) {

        User owner = userRepository.findById(dto.getTurfOwnerId())
                .orElseThrow(() -> new RuntimeException("Turf Owner not found"));

        Turf turf = new Turf();
        turf.setTurfName(dto.getTurfName());
        turf.setLocation(dto.getLocation());
        turf.setCity(dto.getCity());
        turf.setDescription(dto.getDescription());

        // enum handling
        turf.setTurfStatus(
                dto.getTurfStatus() != null
                        ? TurfStatus.valueOf(dto.getTurfStatus().toUpperCase())
                        : TurfStatus.Active
        );

        turf.setTurfOwner(owner); // FK mapping

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
