package com.bookmyturf.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.bookmyturf.entity.Booking;
import com.bookmyturf.entity.SlotMaster;
import com.bookmyturf.repository.BookingRepository;
import com.bookmyturf.repository.SlotMasterRepository;

import jakarta.transaction.Transactional;

@Service
public class BookingService {

    @Autowired
    private BookingRepository bookingRepository;

    @Autowired
    private SlotMasterRepository slotRepository;

    @Transactional
    public Booking createBooking(Booking booking) {
        SlotMaster slot = booking.getSlot();

        if (!slot.getIsAvailable()) {
            throw new RuntimeException("Slot already booked");
        }

        slot.setIsAvailable(false);
        slotRepository.save(slot);

        return bookingRepository.save(booking);
    }

    public List<Booking> getUserBookings(Integer userId) {
        return bookingRepository.findByUser_UserID(userId);
    }
}
