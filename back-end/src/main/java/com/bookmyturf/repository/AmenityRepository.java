

	package com.bookmyturf.repository;

	import com.bookmyturf.entity.Amenity;
	import org.springframework.data.jpa.repository.JpaRepository;

	import java.util.Optional;

	public interface AmenityRepository extends JpaRepository<Amenity, Integer> {

	    Optional<Amenity> findByAmenityName(String amenityName);

	    boolean existsByAmenityName(String amenityName);
	}


