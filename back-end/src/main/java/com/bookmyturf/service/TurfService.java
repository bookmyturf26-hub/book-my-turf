package com.bookmyturf.service;

import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.bookmyturf.entity.Turf;
import com.bookmyturf.entity.User;
import com.bookmyturf.enums.TurfStatus;
import com.bookmyturf.repository.TurfRepository;
import com.bookmyturf.repository.UserRepository;

@Service
public class TurfService {

    @Autowired
    private TurfRepository turfRepository;
    
    // REMOVED: @Autowired private TurfService turfService; 
    // You cannot autowire a service inside itself!

    @Autowired
    private UserRepository userRepository;

    public Turf registerNewTurf(Turf turf) {
        Integer ownerId = turf.getTurfOwner().getUserID(); 
        User owner = userRepository.findById(ownerId)
                .orElseThrow(() -> new RuntimeException("User with ID " + ownerId + " not found"));
        
        turf.setTurfOwner(owner);
        return turfRepository.save(turf);
    }

    public Turf getById(Integer id) {
        return turfRepository.findById(id).orElse(null);
    }

    public Turf createTurf(Turf turf) {
        return turfRepository.save(turf);
    }

    public List<Turf> getAllTurfs() {
        return turfRepository.findAll();
    }

    public List<Turf> getTurfsByStatus(TurfStatus status) {
        return turfRepository.findByTurfStatus(status);
    }

    public Turf getByOwnerId(Integer ownerId) {
        // Finds the most recently created turf for this owner
        return turfRepository.findTopByTurfOwner_UserIDOrderByTurfIdDesc(ownerId);
    }
}