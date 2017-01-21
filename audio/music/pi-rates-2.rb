use_bpm 140

env = "~/samples/ggj17/"
load_samples env

thunder = env, "thunder"
wind = env, "short_wind"
magic_number = 1200
running = true

define :main_loop do |seed=magic_number, reps=16, dt=0.25, base=:c4, amp_factor=1, harshness=0.3, lead=false|
  use_random_seed seed
  notes = scale(base, :augmented2, num_octaves: dice(2))
  tick_reset_all
  with_fx :echo, mix: 0.5, phase: 0.5 do
    with_fx :bitcrusher, bits: 16 do
      with_synth :pluck do
        reps.times do |it|
          n = notes.choose
          sleep_time = (one_in(3) ? dt : dt * 2)
          play n,
            attack: 0.1, release: sleep_time, release: 0.2, sustain: sleep_time * 2,
            coef: 0.7, pluck_delay: 90,
            cutoff: rrand(100, 130),
            amp: rrand(0.6, 0.8) * amp_factor * 1.5
          puts "note"
          if it == reps - 2 and lead then
            cue :do_boom
          end
          if it % 4 == 0 and lead then
            cue :do_cymbal
          end
          cue :note
          sleep sleep_time
        end
      end
    end
  end
end

define :bg_bass do
  use_synth :blade
  r = (ring :c3, :c4)
  with_fx :krush, res: 0.8 do
    in_thread do
      loop do
        break if not running
        play r.tick, amp: rrand(0.2, 0.25) * 0.2
        sleep 0.5
      end
    end
  end
end

define :bg_swoosh do
  in_thread do
    loop do
      sync :start_loop
      sample :ambi_woosh
    end
  end
end

define :bg_boom do
  in_thread do
    loop do
      sync :do_boom
      with_fx :wobble, mix: 1, wave: 2 do
        sample :bass_voxy_hit_c, amp: 0.5
        puts "boom"
        sleep 0.5
        sample :bass_voxy_hit_c, amp: 0.5
      end
    end
  end
end

define :bg_wind do
  in_thread do
    loop do
      sync :start_loop
      sleep rrand(0, 4)
      sample wind, attack: 2, rate: rrand(0.5, 1), release: 20, amp: 0.5
      puts "wind"
    end
  end
end

define :bg_thunder do
  in_thread do
    loop do
      sync :start_loop
      if one_in(2) then
        sample thunder, amp: 0.75, finish: 0.9
        puts "thunder"
      end
    end
  end
end

define :bg_cymbal do
  in_thread do
    loop do
      sync :do_cymbal
      sample :drum_cymbal_pedal, amp: 0.4
      puts "cymbal"
    end
  end
end

bg_bass
bg_swoosh
bg_wind
bg_thunder
bg_boom
bg_cymbal

live_loop :sound do
  2.times do |x|
    cue :start_loop if x > 0
    puts "intro"
    in_thread do
      main_loop magic_number, 16, 0.5, :c3, 0.5
    end
    main_loop magic_number, 16, 0.5, :c5, 1.0, 0.2, true
  end
  4.times do
    cue :start_loop
    puts "main"
    in_thread do
      main_loop magic_number, 16, 0.5, :c3, 0.5, 0.2
    end
    main_loop 42, 16, 0.5, :c4, 1.0, 0.2, true
  end
  2.times do
    cue :start_loop
    puts "outro"
    in_thread do
      main_loop magic_number, 16, 0.5, :c3, 0.5
    end
    main_loop magic_number, 16, 0.5, :c5, 0.2, 0.2, true
  end
  running = false
  stop
end