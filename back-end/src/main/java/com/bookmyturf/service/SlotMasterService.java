package com.bookmyturf.service;

import java.time.LocalDate;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.bookmyturf.entity.SlotMaster;
import com.bookmyturf.repository.SlotMasterRepository;

@Service
	public class SlotMasterService {

	    @Autowired
	    private SlotMasterRepository slotRepository;

	    public SlotMaster createSlot(SlotMaster slot) {
	        slot.setIsAvailable(true);
	        return slotRepository.save(slot);
	    }

	    public List<SlotMaster> getSlots(Integer turfId, LocalDate date) {
	        return slotRepository.findByTurf_TurfIdAndSlotDate(turfId, date);
	    }
	}



