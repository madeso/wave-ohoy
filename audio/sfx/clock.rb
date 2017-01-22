use_bpm 60

# tick tack clock

live_loop :main do
  
  use_synth :prophet
  c = chord(:c4, '+5').reverse
  tick_reset_all
  2.times do
    with_fx :octaver, mix: 0.5 do
      n = c.tick
      play n,
        release: 0.1, attack: 0,
        res: 0.1, env_curve: 6
    end
    sleep 1
  end
  stop
end