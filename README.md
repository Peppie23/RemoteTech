RemoteTech XF
==========

RemoteTech XF is a variant of RemoteTech, made either for people that want a slightly more forgiving experience or for those that want to drop RemoteTech into an existing game and be able to use their existing satellites/probes.

It makes one minor change that has major implications: antennas are always controllable by the player, even without a connection to Mission Control. This allows the player to simulate what real-life probes do when something unexpected happens: they try to re-establish a connection with home! It's handy for...

* Retracting antennas during aerobraking and extending them when you're done!
* Recovering from mistakes (whoops, ComSat1 just blew up!).
* Adding RemoteTech to an existing game (since when you load them up for the first time, all the probes have their antennas turned off).


Besides making it more forgiving, activating and pointing antennas without a connection is also (arguably) more realistic. In real life, probes have low-gain omni-directional antennas that allow space agencies to easily send simple commands across the solar system (like, "point your main dish over here") and probes generally have the intelligence to attempt to re-establish connection if they lose it. In some ways, standard RemoteTech is less forgiving of simple mistakes than real life interplanetary networks! That was my reason for creating XF.

It also fixes the issue RemoteTech has when being added into games that are already in progress. Specifically, since antennas on pre-existing probes are deactivated, these probes end up dead in space, even if you set up a interplanetary network later! Again, the simple act of allowing the user to activate antennas without connection to KSC fixes this! You'll need to set up your network, but once it's up, your existing probes can connect to it!



Installation
--------------

1. Download the Vanilla RemoteTech and install it.
2. Download RemoteTech XF from Github or Kerbalstuff and install over top of it.


FAQ
----

Q: You should submit this to the RemoteTech people!
A: I did. They rejected the pull request, which is to say they don't want this as an option (even one disabled by default). Hey, it's their mod and they can do what they want. But fortunately, their project is open source, and that means anyone can tweak it, so THANK YOU RemoteTech, for having an open source project!

Q: I want to use RemoteTech XF to bootstrap an existing campaign, but then switch to a regular RemoteTech game.
A: No problem! You can always just install the regular RemoteTech back over top XF. Or you can just add this to the RemoteTech_Settings.cfg file to revert to normal RemoteTech mode: ​[CODE] ControlAntennaWithoutConnection = False[/CODE]

Q: Thank you so much, this is awesome!
A: You're welcome, but it's really the main RemoteTech devs that deserve all the thanks. RemoteTech is fabulously complex and a lot of people have put a lot of work into it. And they made it open source, which is freakin' awesome. They didn't have to do that, but they did, and that lets anyone else make their own little tweaks. So for a variety of reasons, they're the best, and I'm just a hack who made one little change. 

Q: What else should I do to make RemoteTech more fun?
A: I can't suggest enough that you enable Root mode for antenna ranges. You can check out the details in the player's guide. But basically it lets omnidirectional antennas get a lot more range when they're communicating with more powerful antennas (like real life!). I also run with the power consumption halved on my game. Especially in the early game, when you just have the static solar panels, the dishes consume way too much juice. Oh, and unless you want to use MechJeb or KOS, you probably want to turn off speed of light. There's a lot of impossible stuff in Kerbal Space Program already, so I don't think it's a stretch to say that in their universe, light moves infinitely fast. 

Q: What does XF stand for?
A: I'm not sure. Right now, I'm leaning toward Extra Forgiving. 
