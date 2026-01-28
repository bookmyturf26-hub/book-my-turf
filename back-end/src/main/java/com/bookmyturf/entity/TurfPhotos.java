
	package com.bookmyturf.entity;

	import jakarta.persistence.*;
	import org.hibernate.annotations.CreationTimestamp;
	import java.time.LocalDateTime;

	@Entity
	@Table(name = "TURF_PHOTOS")
	public class TurfPhotos {
	    @Id
	    @GeneratedValue(strategy = GenerationType.IDENTITY)
	    @Column(name = "PhotoID")
	    private Integer photoId;

	    @ManyToOne(fetch = FetchType.LAZY)
	    @JoinColumn(name = "TurfID", nullable = false)
	    private Turf turf;

	    @Column(name = "PhotoURL", nullable = false)
	    private String photoUrl;

	    @Column(name = "IsMain")
	    private Boolean isMain = false;

	    @Column(name = "Caption")
	    private String caption;

	    @Column(name = "PhotoType")
	    private String photoType;

	    @CreationTimestamp
	    @Column(name = "UploadDate", updatable = false)
	    private LocalDateTime uploadDate;

		public Integer getPhotoId() {
			return photoId;
		}

		public void setPhotoId(Integer photoId) {
			this.photoId = photoId;
		}

		public Turf getTurf() {
			return turf;
		}

		public void setTurf(Turf turf) {
			this.turf = turf;
		}

		public String getPhotoUrl() {
			return photoUrl;
		}

		public void setPhotoUrl(String photoUrl) {
			this.photoUrl = photoUrl;
		}

		public Boolean getIsMain() {
			return isMain;
		}

		public void setIsMain(Boolean isMain) {
			this.isMain = isMain;
		}

		public String getCaption() {
			return caption;
		}

		public void setCaption(String caption) {
			this.caption = caption;
		}

		public String getPhotoType() {
			return photoType;
		}

		public void setPhotoType(String photoType) {
			this.photoType = photoType;
		}

		public LocalDateTime getUploadDate() {
			return uploadDate;
		}

		public void setUploadDate(LocalDateTime uploadDate) {
			this.uploadDate = uploadDate;
		}

	
	    
	}


