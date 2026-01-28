package com.bookmyturf.entity;

import jakarta.persistence.*;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.LocalTime;
import java.util.Set;

@Entity
@Table(
    name = "SLOT_MASTER",
    uniqueConstraints = @UniqueConstraint(columnNames = {"TurfID","SlotDate","StartTime","EndTime"})
)
public class SlotMaster {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "SlotID")
    private Integer slotId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "TurfID", nullable = false)
    private Turf turf;

    @Column(name = "SlotDate", nullable = false)
    private LocalDate slotDate;

    @Column(name = "StartTime", nullable = false)
    private LocalTime startTime;

    @Column(name = "EndTime", nullable = false)
    private LocalTime endTime;

    @Column(name = "SlotPrice", nullable = false)
    private BigDecimal slotPrice;

    @Column(name = "IsAvailable")
    private Boolean isAvailable = true;

    @OneToMany(mappedBy = "slot", cascade = CascadeType.ALL)
    private Set<Booking> bookings;

	public Integer getSlotId() {
		return slotId;
	}

	public void setSlotId(Integer slotId) {
		this.slotId = slotId;
	}

	public Turf getTurf() {
		return turf;
	}

	public void setTurf(Turf turf) {
		this.turf = turf;
	}

	public LocalDate getSlotDate() {
		return slotDate;
	}

	public void setSlotDate(LocalDate slotDate) {
		this.slotDate = slotDate;
	}

	public LocalTime getStartTime() {
		return startTime;
	}

	public void setStartTime(LocalTime startTime) {
		this.startTime = startTime;
	}

	public LocalTime getEndTime() {
		return endTime;
	}

	public void setEndTime(LocalTime endTime) {
		this.endTime = endTime;
	}

	public BigDecimal getSlotPrice() {
		return slotPrice;
	}

	public void setSlotPrice(BigDecimal slotPrice) {
		this.slotPrice = slotPrice;
	}

	public Boolean getIsAvailable() {
		return isAvailable;
	}

	public void setIsAvailable(Boolean isAvailable) {
		this.isAvailable = isAvailable;
	}

	public Set<Booking> getBookings() {
		return bookings;
	}

	public void setBookings(Set<Booking> bookings) {
		this.bookings = bookings;
	}

   
    
}

