package com.bookmyturf.repository;

import com.bookmyturf.entity.TurfPhotos;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface TurfPhotoRepository extends JpaRepository<TurfPhotos, Integer> {

    List<TurfPhotos> findByTurf_TurfId(Integer turfId);

    List<TurfPhotos> findByTurf_TurfIdAndIsMainTrue(Integer turfId);
}

