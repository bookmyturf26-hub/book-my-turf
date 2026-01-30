package com.bookmyturf.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import com.bookmyturf.entity.Turf;
import com.bookmyturf.enums.TurfStatus;
import java.util.List;

public interface TurfRepository extends JpaRepository<Turf, Integer> {
    // Find all turfs with a specific status (e.g., Inactive)
    List<Turf> findByTurfStatus(TurfStatus status);

    // Find a turf by the Owner's User ID (Used by TurfOwner.jsx)
 
    Turf findTopByTurfOwner_UserIDOrderByTurfIdDesc(Integer userId);
}