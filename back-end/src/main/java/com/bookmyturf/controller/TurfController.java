package com.bookmyturf.controller;

import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import com.bookmyturf.entity.Turf;
import com.bookmyturf.entity.User;
import com.bookmyturf.enums.TurfStatus;
import com.bookmyturf.repository.TurfRepository;
import com.bookmyturf.repository.UserRepository;
import com.bookmyturf.service.TurfService;

@RestController
@RequestMapping("/api/turfs")
@CrossOrigin(origins = "http://localhost:3000")
public class TurfController {

    @Autowired
    private TurfService turfService;

    @Autowired
    private UserRepository userRepository; // Correctly injected

    @Autowired
    private TurfRepository turfRepository; // Correctly injected

    @PostMapping("/register")
    public ResponseEntity<?> registerTurf(@RequestBody Turf turf) {
        try {
            // 1. Check if the incoming JSON has the owner object and ID
            if (turf.getTurfOwner() == null || turf.getTurfOwner().getUserID() == null) {
                return ResponseEntity.badRequest().body("Error: Owner ID is missing from the request.");
            }

            // 2. Fetch the User from DB so Hibernate "manages" this entity
            // This is the step that fixes "The given id must not be null"
            User owner = userRepository.findById(turf.getTurfOwner().getUserID())
                    .orElseThrow(() -> new RuntimeException("User not found with ID: " + turf.getTurfOwner().getUserID()));

            // 3. Set the managed owner and initial status
            turf.setTurfOwner(owner);
            turf.setTurfStatus(TurfStatus.Inactive);

            // 4. Save and return
            Turf savedTurf = turfRepository.save(turf);
            return ResponseEntity.ok(savedTurf);
            
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body("Server Error: " + e.getMessage());
        }
    }

    @GetMapping("/owner/{ownerId}")
    public ResponseEntity<Turf> getByOwner(@PathVariable Integer ownerId) {
        Turf turf = turfService.getByOwnerId(ownerId);
        if (turf != null) {
            return ResponseEntity.ok(turf);
        }
        return ResponseEntity.noContent().build();
    }

    @PutMapping("/{id}/approve")
    public ResponseEntity<String> approveTurf(@PathVariable Integer id) {
        try {
            Turf turf = turfService.getById(id);
            if (turf != null) {
                turf.setTurfStatus(TurfStatus.Active);
                turfService.createTurf(turf);
                return ResponseEntity.ok("Turf approved successfully!");
            }
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Turf not found");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }

    @GetMapping("/status/{status}")
    public List<Turf> getByStatus(@PathVariable String status) {
        try {
            String formattedStatus = status.substring(0, 1).toUpperCase() + status.substring(1).toLowerCase();
            TurfStatus enumStatus = TurfStatus.valueOf(formattedStatus);
            return turfService.getTurfsByStatus(enumStatus);
        } catch (Exception e) {
            return null;
        }
    }
}