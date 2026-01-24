package com.bookmyturf.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import java.util.Optional;
import com.bookmyturf.entity.*;
	public interface TurfRepository extends JpaRepository<Turf, Integer> {
		
	}


