package com.bookmyturf.repository;


	import com.bookmyturf.entity.SlotMaster;
	import org.springframework.data.jpa.repository.JpaRepository;

	import java.time.LocalDate;
	import java.time.LocalTime;
	import java.util.List;
	import java.util.Optional;

	public interface SlotMasterRepository extends JpaRepository<SlotMaster, Integer> {

	    // Get all slots for a turf on a particular date
	    List<SlotMaster> findByTurf_TurfIdAndSlotDate(Integer turfId, LocalDate slotDate);

	    // Check duplicate slot
	    Optional<SlotMaster> findByTurf_TurfIdAndSlotDateAndStartTimeAndEndTime(
	            Integer turfId,
	            LocalDate slotDate,
	            LocalTime startTime,
	            LocalTime endTime
	    );

	    // Get only available slots
	    List<SlotMaster> findByIsAvailableTrue();
	}



