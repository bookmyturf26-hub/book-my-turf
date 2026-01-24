package com.bookmyturf.repository;

import com.bookmyturf.entity.Sports;
import com.bookmyturf.entity.User;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.Optional;

public interface SportsRepository extends JpaRepository<Sports, Integer> {}

