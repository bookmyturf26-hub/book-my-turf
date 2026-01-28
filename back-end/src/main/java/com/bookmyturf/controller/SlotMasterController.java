package com.bookmyturf.controller;

import java.time.LocalDate;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.bookmyturf.entity.SlotMaster;
import com.bookmyturf.service.SlotMasterService;

@RestController
	@RequestMapping("/api/slots")
	public class SlotMasterController {

	    @Autowired
	    private SlotMasterService slotService;

	    @PostMapping
	    public SlotMaster create(@RequestBody SlotMaster slot) {
	        return slotService.createSlot(slot);
	    }

	    @GetMapping("/{turfId}/{date}")
	    public List<SlotMaster> getSlots(
	            @PathVariable Integer turfId,
	            @PathVariable LocalDate date) {
	        return slotService.getSlots(turfId, date);
	    }
	}



