use_synth :mod_pulse

live_loop :main do
  c = chord(:c4, '+5')
  with_fx :reverb, room: 0.5 do
    2.times do
      play c.tick, release: 0.1
      sleep 0.1
    end
  end
  stop
end