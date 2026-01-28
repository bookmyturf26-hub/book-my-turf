
	package com.bookmyturf.dto;

	import java.math.BigDecimal;
	import java.time.LocalDate;
import java.time.LocalDateTime;

	public class BookingResponseDTO {

	    private Integer bookingId;
	    private Integer userId;
	    private Integer slotId;
	    private BigDecimal totalAmount;
	    private String bookingStatus;
	    private String paymentStatus;
	    private LocalDateTime bookingDate;

	    // Getters and Setters
	    public Integer getBookingId() { return bookingId; }
	    public void setBookingId(Integer bookingId) { this.bookingId = bookingId; }

	    public Integer getUserId() { return userId; }
	    public void setUserId(Integer userId) { this.userId = userId; }

	    public Integer getSlotId() { return slotId; }
	    public void setSlotId(Integer slotId) { this.slotId = slotId; }

	    public BigDecimal getTotalAmount() { return totalAmount; }
	    public void setTotalAmount(BigDecimal totalAmount) { this.totalAmount = totalAmount; }

	    public String getBookingStatus() { return bookingStatus; }
	    public void setBookingStatus(String bookingStatus) { this.bookingStatus = bookingStatus; }

	    public String getPaymentStatus() { return paymentStatus; }
	    public void setPaymentStatus(String paymentStatus) { this.paymentStatus = paymentStatus; }

	    public LocalDateTime getBookingDate() {
	        return bookingDate;
	    }
	    public void setBookingDate(LocalDateTime bookingDate) {
	        this.bookingDate = bookingDate;
	    }
	}



